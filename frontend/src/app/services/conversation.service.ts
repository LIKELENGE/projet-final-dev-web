import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { finalize } from 'rxjs';
import { getApiErrorMessage } from './api-error';

export interface Message {
  id: number;
  contenu: string;
  dateHeureMessage: string;
}

@Injectable({ providedIn: 'root' })
export class ConversationService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5124/conversations';

  readonly messages = signal<Message[]>([]);
  readonly chargement = signal(false);
  readonly erreur = signal<string | null>(null);

  chargerMessages(conversationId: number): void {
    this.chargement.set(true);
    this.erreur.set(null);

    this.http
      .get<Message[]>(`${this.apiUrl}/${conversationId}/messages`)
      .pipe(finalize(() => this.chargement.set(false)))
      .subscribe({
        next: (messages) => this.messages.set(messages),
        error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
      });
  }

  envoyerMessage(conversationId: number, utilisateurId: number, contenu: string): void {
    this.chargement.set(true);
    this.erreur.set(null);

    this.http
      .post(`${this.apiUrl}/${conversationId}/messages`, { conversationId, utilisateurId, contenu })
      .pipe(finalize(() => this.chargement.set(false)))
      .subscribe({
        next: () => this.chargerMessages(conversationId),
        error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
      });
  }
}
