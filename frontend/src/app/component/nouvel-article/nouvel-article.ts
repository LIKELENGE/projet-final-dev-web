import { Component, inject } from '@angular/core';
import { FormArray, FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AnnonceService, CreerAnnonceRequest } from '../../services/annonce.service';
import { ChoixService } from '../../services/choix.service';
import { imageUrl } from '../../shared/image-url';
import { UtilisateurService } from '../../services/utilisateur.service';

type ArticleFormValue = {
  categorieId: number | null;
  communeId: number | null;
  nom: string;
  description: string;
  prix: number;
  photos: Array<{
    titre: string;
    lien: string;
  }>;
};

@Component({
  selector: 'app-nouvel-article',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './nouvel-article.html',
  styleUrl: './nouvel-article.css',
})
export class NouvelArticle {
  private readonly formBuilder = inject(FormBuilder);
  private readonly router = inject(Router);
  readonly annonceService = inject(AnnonceService);
  readonly choixService = inject(ChoixService);
  readonly utilisateurService = inject(UtilisateurService);

  readonly annonceForm = this.formBuilder.nonNullable.group({
    categorieId: [null as number | null, Validators.required],
    communeId: [null as number | null],
    nom: ['', [Validators.required, Validators.maxLength(100)]],
    description: [''],
    prix: [0, [Validators.required, Validators.min(0)]],
    photos: this.formBuilder.array([this.creerPhotoForm()]),
  });

  constructor() {
    this.choixService.charger();
  }

  get photos(): FormArray {
    return this.annonceForm.controls.photos;
  }

  ajouterPhoto(): void {
    this.photos.push(this.creerPhotoForm());
  }

  supprimerPhoto(index: number): void {
    this.photos.removeAt(index);
  }

  selectionnerImage(event: Event, index: number): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0];

    if (!file) {
      return;
    }

    const photoForm = this.photos.at(index);
    const titre = String(photoForm.get('titre')?.value ?? '');

    this.annonceService.televerserImage(file, titre, (image) => {
      photoForm.patchValue({
        titre: image.titre ?? titre,
        lien: image.lien,
      });
    });
  }

  imageSrc(lien: unknown): string {
    return imageUrl(String(lien ?? ''));
  }

  creerAnnonce(): void {
    if (this.annonceForm.invalid) {
      this.annonceForm.markAllAsTouched();
      return;
    }

    const valeur = this.annonceForm.getRawValue();

    const categorieId = valeur.categorieId;

    if (categorieId == null) {
      this.annonceForm.controls.categorieId.markAsTouched();
      return;
    }

    const request = this.creerRequest(valeur, categorieId);
    this.annonceService.creer(request, () => this.router.navigateByUrl('/mes-articles'));
  }

  private creerRequest(valeur: ArticleFormValue, categorieId: number): CreerAnnonceRequest {
    const request: CreerAnnonceRequest = {
      categorieId,
      communeId: valeur.communeId,
      nom: valeur.nom,
      description: valeur.description || null,
      prix: valeur.prix,
      photos: valeur.photos
        .filter((photo) => photo.lien.trim().length > 0)
        .map((photo) => ({
          titre: photo.titre || null,
          lien: photo.lien,
        })),
    };

    return request;
  }

  private creerPhotoForm() {
    return this.formBuilder.nonNullable.group({
      titre: [''],
      lien: [''],
    });
  }
}
