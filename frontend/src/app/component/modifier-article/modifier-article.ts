import { Component, OnInit, effect, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AnnonceService, ModifierAnnonceRequest } from '../../services/annonce.service';
import { ChoixService } from '../../services/choix.service';
import { UtilisateurService } from '../../services/utilisateur.service';

@Component({
  selector: 'app-modifier-article',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './modifier-article.html',
  styleUrl: './modifier-article.css',
})
export class ModifierArticle implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly formBuilder = inject(FormBuilder);

  readonly annonceService = inject(AnnonceService);
  readonly choixService = inject(ChoixService);
  readonly utilisateurService = inject(UtilisateurService);
  readonly erreur = signal<string | null>(null);

  private annonceId = 0;
  private formulaireCharge = false;

  readonly articleForm = this.formBuilder.nonNullable.group({
    categorieId: [null as number | null, Validators.required],
    communeId: [null as number | null],
    nom: ['', [Validators.required, Validators.maxLength(100)]],
    description: [''],
    prix: [0, [Validators.required, Validators.min(0)]],
  });

  constructor() {
    effect(() => {
      const annonce = this.annonceService.annonceSelectionnee();

      if (!annonce || annonce.id !== this.annonceId || this.formulaireCharge) {
        return;
      }

      this.articleForm.reset({
        categorieId: annonce.categorie?.id ?? null,
        communeId: annonce.commune?.id ?? null,
        nom: annonce.nom,
        description: annonce.description ?? '',
        prix: annonce.prix,
      });

      this.formulaireCharge = true;
    });
  }

  ngOnInit(): void {
    const annonceId = Number(this.route.snapshot.paramMap.get('annonceId'));

    if (!Number.isInteger(annonceId) || annonceId <= 0) {
      this.erreur.set('Article introuvable.');
      return;
    }

    this.annonceId = annonceId;
    this.choixService.charger();
    this.annonceService.consulter(annonceId);
  }

  enregistrer(): void {
    if (this.articleForm.invalid) {
      this.articleForm.markAllAsTouched();
      return;
    }

    const valeur = this.articleForm.getRawValue();

    if (valeur.categorieId == null) {
      this.articleForm.controls.categorieId.markAsTouched();
      return;
    }

    const request: ModifierAnnonceRequest = {
      categorieId: valeur.categorieId,
      communeId: valeur.communeId,
      nom: valeur.nom,
      description: valeur.description || null,
      prix: valeur.prix,
    };

    this.annonceService.modifier(this.annonceId, request, () => this.router.navigateByUrl('/mes-articles'));
  }
}
