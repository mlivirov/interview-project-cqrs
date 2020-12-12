import { Component } from '@angular/core';
import {FormBuilder, FormGroup} from "@angular/forms";
import {SearchResultEntry} from "./models/search-result-entry.model";
import {SearchEngineService} from "./services/search-engine.service";
import {Observable, Subscription} from "rxjs";
import {catchError, delay, filter, finalize, retryWhen, switchMap, tap} from "rxjs/operators";
import {fromPromise} from "rxjs/internal-compatibility";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  searchForm: FormGroup;
  isLoading: boolean = false;
  resultEntries: Array<SearchResultEntry>;
  searchSubscription: Subscription;

  constructor(private fb: FormBuilder, private searchEngineService: SearchEngineService) {
    this.initForm();
  }

  initForm() {
    this.searchForm = this.fb.group({
      query: []
    });
  }

  submit() {
    if (this.searchSubscription) {
      this.searchSubscription.unsubscribe();
    }

    this.isLoading = true;
    this.searchSubscription = this.searchEngineService.createSearch(this.searchForm.value.query)
      .pipe(
        switchMap(t => this.searchEngineService.getResults(t)
          .pipe(
            retryWhen(errors => errors.pipe(filter(err => (err as HttpErrorResponse).status === 501), delay(5000)))
          )),
        finalize(() => this.isLoading = false)
      )
      .subscribe(t => this.resultEntries = t, () => this.resultEntries = []);
  }
}
