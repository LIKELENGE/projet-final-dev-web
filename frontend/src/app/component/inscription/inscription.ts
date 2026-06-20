import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ChoixService } from '../../services/choix.service';
import { InscriptionRequest, UtilisateurService } from '../../services/utilisateur.service';

@Component({
  selector: 'app-inscription',
  imports: [ReactiveFormsModule],
  templateUrl: './inscription.html',
  styleUrl: './inscription.css',
})
export class Inscription {
  private readonly formBuilder = inject(FormBuilder);
  readonly choixService = inject(ChoixService);
  readonly utilisateurService = inject(UtilisateurService);

  readonly inscriptionForm = this.formBuilder.nonNullable.group({
    nom: ['', Validators.required],
    prenom: ['', Validators.required],
    mail: ['', [Validators.required, Validators.email]],
    tel: [''],
    photoProfil: [''],
    motDePasse: ['', Validators.required],
    dateNaissance: [''],
    codeSexe: [null as number | null],
    communeId: [null as number | null],
  });

  constructor() {
    this.choixService.charger();
  }

  inscrire(): void {
    if (this.inscriptionForm.invalid) {
      this.inscriptionForm.markAllAsTouched();
      return;
    }

    const valeur = this.inscriptionForm.getRawValue();
    const request: InscriptionRequest = {
      nom: valeur.nom,
      prenom: valeur.prenom,
      mail: valeur.mail,
      tel: valeur.tel || null,
      photoProfil: valeur.photoProfil || null,
      motDePasse: valeur.motDePasse,
      dateNaissance: valeur.dateNaissance || null,
      codeSexe: valeur.codeSexe && valeur.codeSexe > 0 ? valeur.codeSexe : null,
      communeId: valeur.communeId && valeur.communeId > 0 ? valeur.communeId : null,
    };

    this.utilisateurService.inscrire(request, () => this.inscriptionForm.reset({
      nom: '',
      prenom: '',
      mail: '',
      tel: '',
      photoProfil: '',
      motDePasse: '',
      dateNaissance: '',
      codeSexe: null,
      communeId: null,
    }));
  }
}
