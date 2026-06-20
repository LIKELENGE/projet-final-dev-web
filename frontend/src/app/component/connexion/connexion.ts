import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { UtilisateurService } from '../../services/utilisateur.service';

@Component({
  selector: 'app-connexion',
  imports: [RouterLink],
  templateUrl: './connexion.html',
  styleUrl: './connexion.css',
})
export class Connexion {
  readonly utilisateurService = inject(UtilisateurService);
  private readonly router = inject(Router);

  login(event: Event, email: string, password: string) {
    event.preventDefault();

    this.utilisateurService.connecter(
      {
        mail: email,
        motDePasse: password,
      },
      () => this.router.navigateByUrl('/articles')
    );
  }
}
