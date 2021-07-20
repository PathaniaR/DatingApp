import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
 baseUrl=environment.apiUrl;
  constructor(private htpp:HttpClient) { }

  getMembers(){
    return this.htpp.get<Member[]>(this.baseUrl + 'users');
  }

  getMember(userName:string){
    return this.htpp.get<Member>(this.baseUrl + 'users/' + userName);
  }
}
