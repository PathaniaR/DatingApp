import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Message } from 'src/app/_models/message';
import { MembersService } from 'src/app/_services/members.service';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
  @Input() messages : Message[];
  @ViewChild('messageForm') messageForm :NgForm;
  @Input()userName:string;
  content:string;
  constructor(public messageService:MessageService) { }

  ngOnInit(): void {
  }

sendMessage(){
this.messageService.sendMessage(this.userName,this.content).subscribe(message =>
  {
    this.messages.push(message);
    this.messageForm.reset();
  });
}

}
