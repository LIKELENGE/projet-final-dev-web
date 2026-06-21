import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CategorieService } from '../../services/categorie.service';

@Component({
  selector: 'app-categorie-delete',
  imports: [RouterLink],
  templateUrl: './categorie-delete.html',
  styleUrl: './categorie-delete.css',
})
export class CategorieDelete implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  readonly categorieService = inject(CategorieService);
  readonly erreur = signal<string | null>(null);

  private categorieId = 0;

  ngOnInit(): void {
    const categorieId = Number(this.route.snapshot.paramMap.get('categorieId'));

    if (!Number.isInteger(categorieId) || categorieId <= 0) {
      this.erreur.set('Categorie introuvable.');
      return;
    }

    this.categorieId = categorieId;
    this.categorieService.chargerCategorie(categorieId);
  }

  supprimer(): void {
    this.categorieService.supprimer(this.categorieId, () => this.router.navigateByUrl('/categories'));
  }
}
