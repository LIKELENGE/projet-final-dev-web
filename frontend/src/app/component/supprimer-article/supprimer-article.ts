import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AnnonceService } from '../../services/annonce.service';
import { UtilisateurService } from '../../services/utilisateur.service';

@Component({
  selector: 'app-supprimer-article',
  imports: [RouterLink],
  templateUrl: './supprimer-article.html',
  styleUrl: './supprimer-article.css',
})
export class SupprimerArticle implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  readonly annonceService = inject(AnnonceService);
  readonly utilisateurService = inject(UtilisateurService);
  readonly erreur = signal<string | null>(null);

  private annonceId = 0;

  ngOnInit(): void {
    const annonceId = Number(this.route.snapshot.paramMap.get('annonceId'));

    if (!Number.isInteger(annonceId) || annonceId <= 0) {
      this.erreur.set('Article introuvable.');
      return;
    }

    this.annonceId = annonceId;
    this.annonceService.consulter(annonceId);
  }

  supprimer(): void {
    this.annonceService.supprimer(this.annonceId, () => this.router.navigateByUrl('/mes-articles'));
  }
}
