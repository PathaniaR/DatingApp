import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { SharedModule } from './_modules/shared.module';
import { TestErrorsComponent } from './_errors/test-errors/test-errors.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { MonitorRequestPerformanceInterceptor } from './_interceptors/monitor-request-performance.interceptor';
import { NotFoundComponent } from './_errors/not-found/not-found.component';
import { ServerErrorComponent } from './_errors/server-error/server-error.component';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { DateInputComponent } from './_forms/date-input/date-input.component';
import { MemberMessagesComponent } from './members/member-messages/member-messages.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    MemberDetailComponent,
    ListsComponent,
    MessagesComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    MemberCardComponent,
    MemberEditComponent,
    TextInputComponent,
    DateInputComponent,
    MemberMessagesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule, 
    HttpClientModule,
    FormsModule,
    SharedModule,
    NgxSpinnerModule,
    ReactiveFormsModule
  ],
  providers: [
   { provide: HTTP_INTERCEPTORS,useClass : ErrorInterceptor,multi:true },
   { provide: HTTP_INTERCEPTORS,useClass : MonitorRequestPerformanceInterceptor,multi:true },
   { provide: HTTP_INTERCEPTORS,useClass : JwtInterceptor,multi:true },
   { provide: HTTP_INTERCEPTORS,useClass : LoadingInterceptor,multi:true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule 
 { 
 }
