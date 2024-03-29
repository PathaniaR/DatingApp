import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { Message } from 'src/app/_models/message';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { MessageService } from 'src/app/_services/message.service';
import { PresenceService } from 'src/app/_services/presence.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit,OnDestroy {
  member:Member;
  @ViewChild('memberTabs',{static : true}) memberTabs : TabsetComponent;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  activeTab : TabDirective;
  messages : Message[] = [];
  user:User;
  constructor(private accountService :AccountService, private messageService : MessageService,
              public presence : PresenceService, private memberService:MembersService,private activatedRoute: ActivatedRoute) {
                this.accountService.currenUser$.pipe(take(1)).subscribe(user => this.user = user);
               }

  ngOnInit(): void {
    this.activatedRoute.data.subscribe((data:any) =>{
      this.member = data.member;
    });
    this.activatedRoute.queryParams.subscribe(params =>{
      params.tab ? this.selectTab(params.tab): this.selectTab(0)
      });
      this.galleryOptions=[
       {
         width : '500px',
         height : '500px',
         imagePercent : 100,
         thumbnailsColumns:4,
         imageAnimation : NgxGalleryAnimation.Slide,
         preview : false
       }
      ];
      this.galleryImages=this.getImages();
  }

  getImages() :NgxGalleryImage[]{
     const imageUrls=[];
     for(const photo of this.member.photos){
       imageUrls.push({
         small : photo?.url,
         medium : photo?.url,
         big : photo?.url
       })
     }

     return imageUrls;
  }

  loadMessages(){
    this.messageService.getMessageThread(this.member.userName).subscribe(messages => this.messages = messages);
  }

  OnTabActivated(data: TabDirective){
    this.activeTab=data;
    if(this.activeTab.heading === 'Messages' && this.messages.length === 0){
     //this.loadMessages();
     this.messageService.createHubConnection(this.user,this.member.userName);
    }
    else{
      this.messageService.stopHubConnection();
    }
  }

  selectTab(tabId :number){
    this.memberTabs.tabs[tabId].active = true;
  }

  ngOnDestroy(): void {
      this.messageService.stopHubConnection();
  }
}
