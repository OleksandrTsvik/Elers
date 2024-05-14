import { stringToBoolean, stringToNumber } from './type-converters.util';

export type QueryParamValue =
  | string
  | number
  | boolean
  | Array<string | number | boolean>;

function getURL(): URL {
  return new URL(window.location.href);
}

function updateURL(url: URL) {
  window.history.replaceState(null, '', url);
}

export function getSearchParams(): URLSearchParams {
  return new URLSearchParams(window.location.search);
}

export function getQueryParam(name: string): string | null {
  return getSearchParams().get(name);
}

export function getQueryParamNum(name: string): number | undefined {
  const paramValue = getSearchParams().get(name);

  return paramValue ? stringToNumber(paramValue) : undefined;
}

export function getQueryParamBool(name: string): boolean {
  const paramValue = getSearchParams().get(name);

  return paramValue ? stringToBoolean(paramValue) : false;
}

export function updateQueryParam(name: string, value: QueryParamValue) {
  const url = getURL();

  if (Array.isArray(value)) {
    if (value.length > 0) {
      url.searchParams.set(name, value[0].toString());
    }

    if (value.length > 1) {
      for (const item of value.slice(1)) {
        url.searchParams.append(name, item.toString());
      }
    }
  } else {
    url.searchParams.set(name, value.toString());
  }

  updateURL(url);
}

export function appendQueryParam(name: string, value: QueryParamValue) {
  const url = getURL();

  if (Array.isArray(value)) {
    for (const item of value) {
      url.searchParams.append(name, item.toString());
    }
  } else {
    url.searchParams.append(name, value.toString());
  }

  updateURL(url);
}

export function deleteQueryParam(name: string, value?: string) {
  const url = getURL();

  if (url.searchParams.has(name, value)) {
    url.searchParams.delete(name, value);
    updateURL(url);
  }
}
