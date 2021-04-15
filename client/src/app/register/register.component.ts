import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
@Output() cancelRegister:EventEmitter<boolean>=new EventEmitter<boolean>();
 model:any={};   
  constructor(private accountService:AccountService) { }

  ngOnInit(): void {
  }

  register(){
    console.log(this.model);
    this.accountService.register(this.model).subscribe(response=>{
       console.log('Response regiser',response);
       this.cancel();
    });
  }

  cancel(){
    console.log('Cancelled');
    this.cancelRegister.emit(false);
  }

}
