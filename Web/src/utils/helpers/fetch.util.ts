import { REACT_APP_API_URL } from '../constants/node-env.constants';

export type FetchInput = string | Request | URL;

export type AsyncStatusOk<T> = {
  status: 'ok';
  data: T;
};

export type AsyncStatusFailed = {
  status: 'failed';
  error: unknown;
};

export type AsyncStatus<T> = AsyncStatusOk<T> | AsyncStatusFailed;

export async function baseFetch(
  input: FetchInput,
  init?: RequestInit,
): Promise<Response> {
  const response = await fetch(input, {
    credentials: 'include',
    ...init,
  });

  return response;
}

export async function fetchWithReauth(
  input: FetchInput,
  init?: RequestInit,
): Promise<Response> {
  let response = await baseFetch(input, init);

  if (response.status === 401) {
    const refreshResponse = await baseFetch(
      REACT_APP_API_URL + '/auth/refresh',
      {
        headers: { 'Content-Type': 'application/json' },
        method: 'PUT',
        body: JSON.stringify({}),
      },
    );

    if (refreshResponse.ok) {
      response = await baseFetch(input, init);
    }
  }

  return response;
}

export async function fetchBlob(
  input: FetchInput,
  init?: RequestInit,
): Promise<AsyncStatus<Blob>> {
  try {
    const response = await fetchWithReauth(input, init);

    if (!response.ok) {
      return { status: 'failed', error: await response.json() };
    }

    return { status: 'ok', data: await response.blob() };
  } catch (error) {
    return { status: 'failed', error: undefined };
  }
}
