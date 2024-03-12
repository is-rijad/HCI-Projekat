export interface NapraviRezervacijuEndpointReq {
  sobaId: number;
  sobaAranzmanId: number;
  brojOsoba: number;
  brojDjece: number;
  datumDolaska: Date;
  datumOdlaska: Date;

}
