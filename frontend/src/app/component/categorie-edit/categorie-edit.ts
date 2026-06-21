import { Component, OnInit, effect, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CategorieService } from '../../services/categorie.service';

@Component({
  selector: 'app-categorie-edit',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './categorie-edit.html',
  styleUrl: './categorie-edit.css',
})
export class CategorieEdit implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly formBuilder = inject(FormBuilder);

  readonly categorieService = inject(CategorieService);
  readonly erreur = signal<string | null>(null);

  private categorieId = 0;
  private formulaireCharge = false;

  readonly categorieForm = this.formBuilder.nonNullable.group({
    nom: ['', [Validators.required, Validators.maxLength(150)]],
  });

  constructor() {
    effect(() => {
      const categorie = this.categorieService.categorieSelectionnee();

      if (!categorie || categorie.id !== this.categorieId || this.formulaireCharge) {
        return;
      }

      this.categorieForm.reset({
        nom: categorie.nom,
      });

      this.formulaireCharge = true;
    });
  }

  ngOnInit(): void {
    const categorieId = Number(this.route.snapshot.paramMap.get('categorieId'));

    if (!Number.isInteger(categorieId) || categorieId <= 0) {
      this.erreur.set('Categorie introuvable.');
      return;
    }

    this.categorieId = categorieId;
    this.categorieService.chargerCategorie(categorieId);
  }

  enregistrer(): void {
    if (this.categorieForm.invalid) {
      this.categorieForm.markAllAsTouched();
      return;
    }

    const valeur = this.categorieForm.getRawValue();
    this.categorieService.modifier(this.categorieId, { nom: valeur.nom }, () => this.router.navigateByUrl('/categories'));
  }
}
