import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';

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
  modalRef: BsModalRef;
  registerForm: FormGroup;

  filtro$: string;
  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.validation();
    this.getEventos();
  }

  get filtro(): string{
    return this.filtro$;
  }
  set filtro(value: string){
    this.filtro$ = value;
    this.eventosFiltrados = this.filtrarEventos(this.filtro$);
  }

  openModalEdit(template: TemplateRef<any>): void{
    this.modalRef = this.modalService.show(template);
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

  validation(): void {
    this.registerForm = this.fb.group({
      tema: ['',  [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      imagemURL: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  salvarAlteracao(): void {
  }
}
