import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountService:AccountService,private toaster:ToastrService){

  }
  canActivate(): Observable<boolean> {
    return this.accountService.currenUser$.pipe(
      map(user=>{
        if(user){
          return true;
        }
        this.toaster.error("Sorry! You are not allowed to navigate to this component.");
      })
    );
  }
  
}
