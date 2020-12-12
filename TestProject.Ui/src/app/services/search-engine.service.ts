import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {SearchResultEntry} from "../models/search-result-entry.model";
import {environment} from "../../environments/environment";

@Injectable({ providedIn: "root" })
export class SearchEngineService {
  constructor(private httpClient: HttpClient) {
  }

  createSearch(searchPhrase: string): Observable<number> {
    return this.httpClient.post<number>(`${environment.apiBaseUrl}/search`,{
      searchPhrase: searchPhrase
    });
  }

  getResults(searchId: number): Observable<Array<SearchResultEntry>> {
    return this.httpClient.get<Array<SearchResultEntry>>(`${environment.apiBaseUrl}/search?searchId=${searchId}`);
  }
}
