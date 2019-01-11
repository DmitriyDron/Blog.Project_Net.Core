import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { TagInputModule } from 'ngx-chips';
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { ToastyModule } from 'ng2-toasty';
import { CategoriesComponent } from './components/categories/categories.component';
import { BlogComponent } from './components/blog/blog.component';
import { PostviewComponent } from './components/post-view/post-view.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { PostformComponent } from './components/post-form/post-form.component';
import { PostsComponent } from './components/posts/posts.component';
import { PostService } from './services/post.service';
import { CategoryService } from './services/category.service';
import { TagService } from './services/tag.service';
import { GlobalEventsService } from './services/global-events.service';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        PostsComponent,
        PostformComponent,
        PaginationComponent,
        PostviewComponent,
        BlogComponent,
        CategoriesComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        TagInputModule,
        ReactiveFormsModule,
        ToastyModule.forRoot(),
        RouterModule.forRoot([
            { path: '', component: BlogComponent, pathMatch: 'full' },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'tag/:id', component: BlogComponent },
            { path: 'posts', component: PostsComponent},
            { path: 'posts/categories', component: CategoriesComponent },
           // { path: 'login', component: LoginComponent },
            { path: 'post/create', component: PostformComponent},
            { path: 'post/:id', component: PostviewComponent },
            { path: 'post/edit/:id', component: PostformComponent},
           
            { path: 'error', redirectTo: '' },
            { path: 'home', redirectTo: '' },
            { path: '**', redirectTo: '' }
        ])
    ],
    providers: [PostService, CategoryService, TagService, GlobalEventsService]
})
export class AppModuleShared {
}