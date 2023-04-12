import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, ValidatorFn, Validators } from '@angular/forms';
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
 registerForm:UntypedFormGroup;
 errors : string[] = [];

  constructor(private router : Router, private accountService:AccountService,private toastrService:ToastrService,private formBuilder : UntypedFormBuilder) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.registerForm=this.formBuilder.group({
      username : new UntypedFormControl('',Validators.required),
      gender : new UntypedFormControl('Male'),
      knownAs : new UntypedFormControl('',Validators.required),
      dateOfBirth : new UntypedFormControl('',Validators.required),
      city : new UntypedFormControl('',Validators.required),
      country : new UntypedFormControl('',Validators.required),
      password: new UntypedFormControl('',[Validators.required,Validators.minLength(4),Validators.maxLength(8)]),
      confirmpassword: new UntypedFormControl('',[Validators.required,this.matchValues('password')])
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
