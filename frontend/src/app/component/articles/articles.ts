import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AnnonceService } from '../../services/annonce.service';
import { ChoixService } from '../../services/choix.service';
import { imageUrl } from '../../shared/image-url';


@Component({
  selector: 'app-articles',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './articles.html',
  styleUrl: './articles.css',
})
export class Articles implements OnInit {
  private readonly formBuilder = inject(FormBuilder);
  readonly annonceService = inject(AnnonceService);
  readonly choixService = inject(ChoixService);

  readonly rechercheForm = this.formBuilder.nonNullable.group({
    recherche: [''],
    categorieId: [null as number | null],
  });

  ngOnInit(): void {
    this.choixService.charger();
    this.annonceService.rechercher();
  }

  rechercher(): void {
    const valeur = this.rechercheForm.getRawValue();

    this.annonceService.rechercher(valeur.recherche, valeur.categorieId);
  }

  imageSrc = imageUrl;
}
