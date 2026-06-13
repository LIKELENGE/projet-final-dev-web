import { Component } from '@angular/core';

@Component({
  selector: 'app-connexion',
  imports: [],
  templateUrl: './connexion.html',
  styleUrl: './connexion.css',
})
export class Connexion {
  login(event: Event, email: string, password: string) {
    event.preventDefault();

    console.log('Email:', email);
    console.log('Mot de passe:', password);
  }
}
