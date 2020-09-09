import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventosFiltrados: any = [];
   eventos: any = [];
   imagemLargura = 50;
   imagemMargem = 2;
   mostrarImagem = false;

   filtro$: string;
   get filtro(): string{
     return this.filtro$;
   }
   set filtro(value: string){
     this.filtro$ = value;
     this.eventosFiltrados = this.filtrarEventos(this.filtro$);
   }

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getEventos();
  }

  filtrarEventos(filtro: string): any{
    if (filtro){
      filtro = filtro.toLocaleLowerCase();
      return this.eventos.filter(
        evento => evento.tema.toLocaleLowerCase().indexOf(filtro) !== -1
      );
    }
    else{
      return this.eventos;
    }
  }

  getEventos(): void {
    this.eventos = this.http.get('http://localhost:5000/evento').subscribe(
      response =>  {
        this.eventos = response;
      }
      , error =>  {
        console.log(error);
      }
    );
  }

  alternarImagem(): void{
    this.mostrarImagem = !this.mostrarImagem;
  }
}
