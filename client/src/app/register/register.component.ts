import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
@Output() cancelRegister:EventEmitter<boolean>=new EventEmitter<boolean>();   
 registerForm:FormGroup;
 errors : string[] = [];

  constructor(private router : Router, private accountService:AccountService,private toastrService:ToastrService,private formBuilder : FormBuilder) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.registerForm=this.formBuilder.group({
      username : new FormControl('',Validators.required),
      gender : new FormControl('Male'),
      knownAs : new FormControl('',Validators.required),
      dateOfBirth : new FormControl('',Validators.required),
      city : new FormControl('',Validators.required),
      country : new FormControl('',Validators.required),
      password: new FormControl('',[Validators.required,Validators.minLength(4),Validators.maxLength(8)]),
      confirmpassword: new FormControl('',[Validators.required,this.matchValues('password')])
    });

    this.registerForm.controls.password.valueChanges.subscribe(x=>{
      this.registerForm.controls.confirmpassword.updateValueAndValidity();

    });
  }

  matchValues(matchTo:string):ValidatorFn{
    return (control :AbstractControl)=>{
      return control?.value === control?.parent?.controls[matchTo].value ? null : {isMatching: true};
    }
  }

  register(){
    console.log(this.registerForm.value);
    this.accountService.register(this.registerForm.value).subscribe(response=>{
       this.router.navigateByUrl('/members');
    },(error:any)=>{
      this.errors = error;
      this.toastrService.error(error.error);
    });
  }

  cancel(){
    console.log('Cancelled');
    this.cancelRegister.emit(false);
  }

}
