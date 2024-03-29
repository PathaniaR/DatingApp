import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators'
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { PresenceService } from './presence.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl=environment.apiUrl;

  private currentUserSource=new ReplaySubject<User>(1);
  currenUser$=this.currentUserSource.asObservable();

  constructor(private http:HttpClient,private presenceService:PresenceService) {
   }

   login(model:any){
     return this.http.post(this.baseUrl + 'account/login',model).pipe(
       map((response:User)=>{
         const user=response;
         if(user){
           localStorage.setItem('user',JSON.stringify(user));
           this.currentUserSource.next(user);
           this.presenceService.CreateHubConnection(user);
         }
       })
     );
   }

   register(model:any){
     return this.http.post(this.baseUrl + 'account/register',model).pipe(
       map((user : User) =>{
        if(user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user);
          this.presenceService.CreateHubConnection(user);
          return user;
        }
       })
     )
   }

   setCurrentUser(user:User){
     this.currentUserSource.next(user);
   }

   logout(){
     localStorage.removeItem('user');
     this.currentUserSource.next(null);
     this.presenceService.stopHubConnection();
   }
}
