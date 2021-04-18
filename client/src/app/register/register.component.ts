import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
@Output() cancelRegister:EventEmitter<boolean>=new EventEmitter<boolean>();
 model:any={};   
  constructor(private accountService:AccountService,private toastrService:ToastrService) { }

  ngOnInit(): void {
  }

  register(){
    console.log(this.model);
    this.accountService.register(this.model).subscribe(response=>{
       console.log('Response regiser',response);
       this.cancel();
    },(error:any)=>{
      this.toastrService.error(error.error);
    });
  }

  cancel(){
    console.log('Cancelled');
    this.cancelRegister.emit(false);
  }

}
