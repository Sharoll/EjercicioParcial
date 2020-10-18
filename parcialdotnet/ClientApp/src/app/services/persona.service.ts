import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HandleHttpErrorService } from '../@base/handle-http-error.service';
import { Persona } from '../Ayuda/models/persona';
import { tap, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PersonaService {

  baseUrl: string;

  constructor(
    
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl:string,
    private handleErrorService: HandleHttpErrorService)

   {
     this.baseUrl = baseUrl;

   }

  get(): Observable<Persona[]>{
    return this.http.get<Persona[]>(this.baseUrl + 'api/Persona')
    .pipe(
      tap(_ => this.handleErrorService.log('datos enviados')),
      catchError(this.handleErrorService.handleError<Persona[]>('Consulta Persona', null))
      );
  }
    /*Tope(persona:Persona,personas:Persona[]):boolean{
        var sumador=persona.valorApoyo;
        for(var aux in personas){
          sumador=sumador+personas[aux].valorApoyo;
        }
        if(sumador<=600000000){
          return true;
        }else{
          return false;
        }
    }*/
    post(persona: Persona): Observable<Persona> {
    return this.http.post<Persona>(this.baseUrl + 'api/Persona', persona)
    .pipe(
      tap(_ => this.handleErrorService.log('datos enviados')),
     catchError(this.handleErrorService.handleError<Persona>('Registrar Persona', null))
    );

 }
    /*   
     validarTope(persona: Persona) {
          let personas: Persona[] = [];
          if (this.get() != null) {
            personas = this.get();
          }
        if(this.Tope(persona,personas)){
          personas.push(persona);
          localStorage.setItem('informacion', JSON.stringify(personas));
          return true;
        }else{
          return false;
        }
         
    } */

}
