import { Component, OnInit } from '@angular/core';
import { EventoService } from 'src/app/_services/evento.service';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
import { Evento } from 'src/app/_models/Evento';

@Component({
  selector: 'app-evento-edit',
  templateUrl: './eventoEdit.component.html',
  styleUrls: ['./eventoEdit.component.css']
})
export class EventoEditComponent implements OnInit {
  titulo = 'Editar evento';
  registerForm: FormGroup;
  evento: Evento = new Evento();
  dataEvento: string;
  imagemURL = 'assets/img/upload.jpg';

  get lotes(): FormArray {
    return this.registerForm.get('lotes') as FormArray;
  }

  get redesSociais(): FormArray {
    return this.registerForm.get('redesSociais') as FormArray;
  }

  constructor(
    private eventoService: EventoService,
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.validation();
  }

  validation(): void {
    this.registerForm = this.fb.group({
      tema: ['',  [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      imagemURL: [''],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      lotes: this.fb.array([this.criarLote()]),
      redesSociais: this.fb.array([this.criarRedeSocial()])
    });
  }

  criarLote(): FormGroup {
    return this.fb.group({
      nome : ['', Validators.required],
      quantidade : ['', Validators.required],
      preco : ['', Validators.required],
      dataInicio : [''],
      dataFim : ['']
    });
  }

  criarRedeSocial(): FormGroup {
    return this.fb.group({
      nome : ['', Validators.required],
      url : ['', Validators.required]
    });
  }

  adicionarLote(): void {
    this.lotes.push(this.criarLote());
  }

  adicionarRedeSocial(): void {
    this.redesSociais.push(this.criarRedeSocial());
  }

  removerLote(id: number): void {
    this.lotes.removeAt(id);
  }

  removerRedeSocial(id: number): void {
    this.redesSociais.removeAt(id);
  }

  onFileChange(file: FileList): void {
    const reader = new FileReader();
    reader.onload = (event: any) => this.imagemURL = event.target.result;
    reader.readAsDataURL(file[0]);
  }
}
