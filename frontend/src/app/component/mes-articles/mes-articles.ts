import { Component, OnInit, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Annonce, AnnonceService, ModifierAnnonceRequest } from '../../services/annonce.service';
import { ChoixService } from '../../services/choix.service';
import { imageUrl } from '../../shared/image-url';
import { UtilisateurService } from '../../services/utilisateur.service';

@Component({
  selector: 'app-mes-articles',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './mes-articles.html',
  styleUrl: './mes-articles.css',
})
export class MesArticles implements OnInit {
  private readonly formBuilder = inject(FormBuilder);
  readonly annonceService = inject(AnnonceService);
  readonly choixService = inject(ChoixService);
  readonly utilisateurService = inject(UtilisateurService);
  readonly annonceEnEdition = signal<Annonce | null>(null);

  readonly modificationForm = this.formBuilder.nonNullable.group({
    categorieId: [null as number | null, Validators.required],
    communeId: [null as number | null],
    nom: ['', [Validators.required, Validators.maxLength(100)]],
    description: [''],
    prix: [0, [Validators.required, Validators.min(0)]],
  });

  ngOnInit(): void {
    this.choixService.charger();
    this.annonceService.chargerMesAnnonces();
  }

  modifier(annonce: Annonce): void {
    this.annonceEnEdition.set(annonce);
    this.modificationForm.reset({
      categorieId: annonce.categorie?.id ?? null,
      communeId: annonce.commune?.id ?? null,
      nom: annonce.nom,
      description: annonce.description ?? '',
      prix: annonce.prix,
    });
  }

  annulerModification(): void {
    this.annonceEnEdition.set(null);
    this.modificationForm.reset({
      categorieId: null,
      communeId: null,
      nom: '',
      description: '',
      prix: 0,
    });
  }

  enregistrerModification(): void {
    const annonce = this.annonceEnEdition();

    if (!annonce) {
      return;
    }

    if (this.modificationForm.invalid) {
      this.modificationForm.markAllAsTouched();
      return;
    }

    const valeur = this.modificationForm.getRawValue();

    if (valeur.categorieId == null) {
      this.modificationForm.controls.categorieId.markAsTouched();
      return;
    }

    const request: ModifierAnnonceRequest = {
      categorieId: valeur.categorieId,
      communeId: valeur.communeId,
      nom: valeur.nom,
      description: valeur.description || null,
      prix: valeur.prix,
    };

    this.annonceService.modifier(annonce.id, request, () => this.annulerModification());
  }

  supprimer(annonceId: number): void {
    this.annonceService.supprimer(annonceId);
  }

  imageSrc = imageUrl;
}
