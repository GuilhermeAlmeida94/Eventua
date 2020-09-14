import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';
import { ToastrService } from 'ngx-toastr';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { defineLocale, ptBrLocale } from 'ngx-bootstrap/chronos';
defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
  titulo = 'Eventos';
  eventosFiltrados: Evento[];
  eventos: Evento[];
  evento: Evento;
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  registerForm: FormGroup;
  ehInsersao: boolean;
  bodySmallTemplate: string;
  dataEvento: string;

  filtro$: string;
  constructor(
    private eventoService: EventoService,
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private toastr: ToastrService
  ) {
    this.localeService.use('pt-br');
   }

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

  inserirEvento(template: any): void{
    this.openModal(template);
    this.ehInsersao = true;
  }

  editarEvento(template: any, evento: Evento): void {
    this.openModal(template);
    this.ehInsersao = false;
    this.evento = evento;
    this.registerForm.patchValue(this.evento);
  }

  apagarEvento(template: any, evento: Evento): void {
    this.openModal(template);
    this.evento = evento;
    this.bodySmallTemplate = 'Tem certeza que quer apagar este evento?';
  }

  openModal(template: any): void{
    this.registerForm.reset();
    template.show();
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
    }
    , error =>  {
        this.toastr.error(`Erro ao carregar: ${error}`);
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

  salvarAlteracao(template: any): void {
    if (this.registerForm.valid) {
      if (this.ehInsersao){
        this.evento = Object.assign({}, this.registerForm.value);
        this.eventoService.postEvento(this.evento).subscribe(
          (novoEvento: Evento) => {
            template.hide();
            this.toastr.success('Evento inserido com sucesso.');
            this.getEventos();
          }, error => {
            this.toastr.error(`Erro ao inserir: ${error}`);
            console.log(error);
          }
        );
      }
      else {
        this.evento = Object.assign({id: this.evento.id}, this.registerForm.value);
        this.eventoService.putEvento(this.evento.id, this.evento).subscribe(
          (novoEvento: Evento) => {
            template.hide();
            this.toastr.success('Evento atualizado com sucesso.');
            this.getEventos();
          }, error => {
            this.toastr.error(`Erro ao atualizar: ${error}`);
            console.log(error);
          }
        );
      }
    }
  }

  confirmarApagar(template: any): void {
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
        template.hide();
        this.toastr.success('Evento apagado com sucesso.');
        this.getEventos();
      }, error => {
        this.toastr.error(`Erro ao apagar: ${error}`);
        console.log(error);
      }
    );
  }
}
