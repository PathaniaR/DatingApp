import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { NotFoundComponent } from './_errors/not-found/not-found.component';
import { ServerErrorComponent } from './_errors/server-error/server-error.component';
import { TestErrorsComponent } from './_errors/test-errors/test-errors.component';
import { AuthGuard } from './_guards/auth.guard';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';

const routes: Routes = [
  {path : '', component :  HomeComponent},
  {
    path :'',
    runGuardsAndResolvers : 'always',
    canActivate:[AuthGuard],
    children :[
      {path : 'members', component :  MemberListComponent},
      {path : 'members/:username', component :  MemberDetailComponent , resolve : {member : MemberDetailResolver}},
      {path : 'member/edit', component :  MemberEditComponent,canDeactivate : [PreventUnsavedChangesGuard]},
      {path : 'lists', component :  ListsComponent},
      {path : 'messages', component :  MessagesComponent},
      {path : 'not-found', component :  NotFoundComponent},
      {path : 'server-error' ,component : ServerErrorComponent}
    ]
  },
  {path : 'error',component:TestErrorsComponent},
  {path : '**', component :  NotFoundComponent,pathMatch : 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
