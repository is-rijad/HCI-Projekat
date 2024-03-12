export interface KorisnickiNalog {
  id: number;
  email: string;
  lozinka: string;
  ime: string;
  prezime: string;
  datumRodjenja: Date;
  slikaKorisnika?: string;

  isGost: boolean;
  isMenadzer: boolean;
  isAdmin: boolean;
}
