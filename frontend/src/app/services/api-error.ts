import { HttpErrorResponse } from '@angular/common/http';

export function getApiErrorMessage(error: unknown): string {
  if (error instanceof HttpErrorResponse) {
    const apiError = error.error as { error?: string } | null;

    if (apiError?.error) {
      return apiError.error;
    }

    if (error.status === 0) {
      return "Impossible de contacter l'API. Verifiez que le backend est lance.";
    }

    if (error.status === 401) {
      return 'Vous devez etre connecte pour effectuer cette action.';
    }
  }

  return 'Une erreur est survenue.';
}
