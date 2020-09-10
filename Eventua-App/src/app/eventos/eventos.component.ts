import { Component, OnInit } from '@angular/core';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventosFiltrados: Evento[];
  eventos: Evento[];
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

  constructor(private eventoService: EventoService) { }

  ngOnInit(): void {
    this.getEventos();
  }

  filtrarEventos(filtro: string): Evento[]{
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
    this.eventoService.getAllEvento().subscribe((evento$: Evento[]) =>  {
      this.eventos = evento$;
      this.eventosFiltrados = this.eventos;
      console.log(evento$);
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
