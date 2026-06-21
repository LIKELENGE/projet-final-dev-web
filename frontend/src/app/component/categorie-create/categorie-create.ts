import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CategorieService } from '../../services/categorie.service';

@Component({
  selector: 'app-categorie-create',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './categorie-create.html',
  styleUrl: './categorie-create.css',
})
export class CategorieCreate {
  private readonly formBuilder = inject(FormBuilder);
  private readonly router = inject(Router);

  readonly categorieService = inject(CategorieService);

  readonly categorieForm = this.formBuilder.nonNullable.group({
    nom: ['', [Validators.required, Validators.maxLength(150)]],
  });

  creer(): void {
    if (this.categorieForm.invalid) {
      this.categorieForm.markAllAsTouched();
      return;
    }

    const valeur = this.categorieForm.getRawValue();
    this.categorieService.creer({ nom: valeur.nom }, () => this.router.navigateByUrl('/categories'));
  }
}
