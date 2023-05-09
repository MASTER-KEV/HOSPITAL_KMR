export * from './login.service';
import { LoginService } from './login.service';
export * from './usuarios.service';
import { UsuariosService } from './usuarios.service';
export const APIS = [LoginService, UsuariosService];
