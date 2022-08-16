import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { UserParams } from 'src/app/_models/userParams';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styles: [
  ]
})
export class MemberListComponent implements OnInit {
 members : Member[];
 pagination :Pagination;
 userParams :UserParams;
 user :User;
 genderList =[{value : 'male',display : 'Male'},{value : 'female',display : 'Female'}];

  constructor(private memberService:MembersService) {
   this.userParams=this.memberService.getUserParams();
   }

   resetFilters(){
     this.userParams=this.memberService.retsetUserParams();
     this.loadMembers();
   }

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers(){
    this.memberService.setUserParams(this.userParams);
    this.memberService.getMembers(this.userParams).subscribe(response =>{
    this.members = response.result;
    this.memberService.members=this.members;
    this.pagination = response.pagination;
    });
  }

  onPageChanged(event : any){
      this.userParams.pageNumber=event.page;
      this.memberService.setUserParams(this.userParams);
      this.loadMembers();
  }
}
