import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class PresenceService {
  hubUrl = environment.hubUrl;
  private hubConnection:HubConnection;
  private onlineUsersSource = new BehaviorSubject<string[]>([]);
  onlineUsers$= this.onlineUsersSource.asObservable();

  constructor(private toastr : ToastrService) { }

  CreateHubConnection(user : User){
  this.hubConnection = new HubConnectionBuilder()
                           .withUrl(this.hubUrl + 'presence',{
                           accessTokenFactory: ()=>user.token
                           }).withAutomaticReconnect().build();
  this.hubConnection.start().catch(error => console.log(error));
  this.hubConnection.on('UserIsOnline',userName =>{
    this.toastr.info(userName + 'is connected');
  });
  this.hubConnection.on('UserIsOffline',userName =>{
    this.toastr.warning(userName + 'has disconnected');
  });
  this.hubConnection.on('GetAllOnlineUsers',(onlineUsers : string[]) =>{
  this.onlineUsersSource.next(onlineUsers);
  });
}
stopHubConnection(){
  this.hubConnection.stop().catch(error => console.log(error));
}
}
