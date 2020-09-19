import { Injectable } from '@angular/core';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { Evento } from '../_models/Evento';
import { Observable } from 'rxjs';
import { Identifiers } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  baseUrl = 'http://localhost:5000/evento';

  constructor(private http: HttpClient) {  }

  getAllEvento(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.baseUrl);
  }

  getEventoById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseUrl}/${id}`);
  }

  getEventoByTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseUrl}/getByTema/${tema}`);
  }

  postEvento(evento: Evento): any {
    return this.http.post(this.baseUrl, evento);
  }

  postUpload(file: File, nomeArquivo: string): any {
    const filetToUpload = file[0] as File;
    const formData = new FormData();
    formData.append('file', filetToUpload, nomeArquivo);

    return this.http.post(`${this.baseUrl}/upload`, formData);
  }

  putEvento(id: number, evento: Evento): any {
    return this.http.put(`${this.baseUrl}/${id}`, evento);
  }

  deleteEvento(id: number): any {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
