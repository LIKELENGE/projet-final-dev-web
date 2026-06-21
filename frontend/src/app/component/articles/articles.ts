import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AnnonceService } from '../../services/annonce.service';
import { ChoixService } from '../../services/choix.service';
import { imageUrl } from '../../shared/image-url';
import { Pagination } from '../../shared/pagination/pagination';


@Component({
  selector: 'app-articles',
  imports: [ReactiveFormsModule, RouterLink, Pagination],
  templateUrl: './articles.html',
  styleUrl: './articles.css',
})
export class Articles implements OnInit {
  readonly taillePage = 6;
  private readonly formBuilder = inject(FormBuilder);
  readonly annonceService = inject(AnnonceService);
  readonly choixService = inject(ChoixService);

  readonly rechercheForm = this.formBuilder.nonNullable.group({
    recherche: [''],
    categorieId: [null as number | null],
  });

  ngOnInit(): void {
    this.choixService.charger();
    this.rechercher();
  }

  rechercher(page = 1): void {
    const valeur = this.rechercheForm.getRawValue();

    this.annonceService.rechercher(valeur.recherche, valeur.categorieId, page, this.taillePage);
  }

  imageSrc = imageUrl;
}
