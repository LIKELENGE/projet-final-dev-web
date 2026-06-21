import { Component, OnInit, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AnnonceService } from '../../services/annonce.service';
import { imageUrl } from '../../shared/image-url';
import { UtilisateurService } from '../../services/utilisateur.service';
import { Pagination } from '../../shared/pagination/pagination';

@Component({
  selector: 'app-mes-articles',
  imports: [RouterLink, Pagination],
  templateUrl: './mes-articles.html',
  styleUrl: './mes-articles.css',
})
export class MesArticles implements OnInit {
  readonly taillePage = 6;
  readonly annonceService = inject(AnnonceService);
  readonly utilisateurService = inject(UtilisateurService);

  ngOnInit(): void {
    this.chargerPage();
  }

  chargerPage(page = 1): void {
    this.annonceService.chargerMesAnnonces(page, this.taillePage);
  }

  imageSrc = imageUrl;
}
