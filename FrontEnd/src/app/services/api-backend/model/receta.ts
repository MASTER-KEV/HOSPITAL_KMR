/**
 * Api
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */
import { MedicamentosRecetum } from './medicamentosRecetum';
import { Cita } from './cita';
import { Usuario } from './usuario';


export interface Receta { 
    idReceta?: number;
    idCita?: number;
    idUsuario?: number;
    idCitaNavigation?: Cita;
    idUsuarioNavigation?: Usuario;
    medicamentosReceta?: Array<MedicamentosRecetum> | null;
}

