import { Injectable } from '@angular/core';
import { Persona } from '../Ayuda/models/persona';

@Injectable({
  providedIn: 'root'
})
export class PersonaService {

  constructor() { }

  get(): Persona[] {
    return JSON.parse(localStorage.getItem('informacion'));
  }
    validarPersona (persona:Persona,personas:Persona[]):boolean{
      for (var i in personas){
        if (persona.identificacion==personas[i].identificacion){
          return false;
        }  
      }
      return true;
    }
    Tope(persona:Persona,personas:Persona[]):boolean{
        var sumador=persona.valorApoyo;
        for(var aux in personas){
          sumador=sumador+personas[aux].valorApoyo;
        }
        if(sumador<=600000000){
          return true;
        }else{
          return false;
        }
    }
    post(persona: Persona) {
          let personas: Persona[] = [];
          if (this.get() != null) {
            personas = this.get();
          }
      return this.validarPersona(persona,personas);  
    }     
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
         
    } 

}
