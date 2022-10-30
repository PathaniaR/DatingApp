import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Message } from '../_models/message';
import { User } from '../_models/user';
import { getPaginatedResults, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl:string = environment.apiUrl;
  hubUrl=environment.hubUrl;
  private hubConnection : HubConnection;
  private messageThreadSource = new BehaviorSubject<Message[]>([]);
  messageThread$= this.messageThreadSource.asObservable();

  constructor(private http: HttpClient) { }

  getMessages(pageNumber,pageSize,container){
   let params=getPaginationHeaders(pageNumber,pageSize);
   params=params.append('Container',container);
   return getPaginatedResults<Message[]>(this.baseUrl + 'messages',params,this.http);
  }

  createHubConnection(user:User,otherUserName :string){
    this.hubConnection = new HubConnectionBuilder()
                             .withUrl(this.hubUrl + 'messages?user=' + otherUserName,{
                               accessTokenFactory : () => user.token
                             }).withAutomaticReconnect().build();
    this.hubConnection.start().catch(error => console.log(error));
    this.hubConnection.on('RecieveMessageThread',messages => this.messageThreadSource.next(messages));
  }

  stopHubConnection(){
    this.hubConnection.stop();
  }

  getMessageThread(userName:string){
    return this.http.get<Message[]>(this.baseUrl + "messages/thread/" + userName);
  }

  sendMessage(userName:string,content :string){
    return this.http.post<Message>(this.baseUrl + 'messages',{RecipientUserName :userName ,Content:content});
  }

  deleteMessage(id : number){
    return this.http.delete(this.baseUrl + "messages/" + id);
  }
}
