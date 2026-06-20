import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { finalize } from 'rxjs';
import { getApiErrorMessage } from './api-error';

export interface InscriptionRequest {
  nom: string;
  prenom: string;
  mail: string;
  tel?: string | null;
  photoProfil?: string | null;
  motDePasse: string;
  dateNaissance?: string | null;
  codeSexe?: number | null;
  communeId?: number | null;
}

export interface ConnexionRequest {
  mail: string;
  motDePasse: string;
}

export interface UtilisateurConnecte {
  id: number;
  nom: string;
  prenom: string;
  mail: string;
  token: string;
}

@Injectable({ providedIn: 'root' })
export class UtilisateurService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5124/utilisateurs';
  private readonly storageKey = 'marketplace_utilisateur';

  readonly chargement = signal(false);
  readonly erreur = signal<string | null>(null);
  readonly message = signal<string | null>(null);
  readonly utilisateur = signal<UtilisateurConnecte | null>(this.getUtilisateurStocke());

  get token(): string | null {
    return this.utilisateur()?.token ?? null;
  }

  inscrire(request: InscriptionRequest, apresInscription: () => void): void {
    this.chargement.set(true);
    this.erreur.set(null);
    this.message.set(null);

    this.http
      .post(`${this.apiUrl}/inscription`, request)
      .pipe(finalize(() => this.chargement.set(false)))
      .subscribe({
        next: () => {
          this.message.set('Compte cree avec succes.');
          apresInscription();
        },
        error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
      });
  }

  connecter(request: ConnexionRequest, apresConnexion: () => void): void {
    this.chargement.set(true);
    this.erreur.set(null);
    this.message.set(null);

    this.http
      .post<UtilisateurConnecte>(`${this.apiUrl}/connexion`, request)
      .pipe(finalize(() => this.chargement.set(false)))
      .subscribe({
        next: (utilisateur) => {
          this.utilisateur.set(utilisateur);
          localStorage.setItem(this.storageKey, JSON.stringify(utilisateur));
          this.message.set('Connexion reussie.');
          apresConnexion();
        },
        error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
      });
  }

  deconnecter(): void {
    this.utilisateur.set(null);
    localStorage.removeItem(this.storageKey);
  }

  private getUtilisateurStocke(): UtilisateurConnecte | null {
    const valeur = localStorage.getItem(this.storageKey);

    if (!valeur) {
      return null;
    }

    try {
      return JSON.parse(valeur) as UtilisateurConnecte;
    } catch {
      localStorage.removeItem(this.storageKey);
      return null;
    }
  }
}
