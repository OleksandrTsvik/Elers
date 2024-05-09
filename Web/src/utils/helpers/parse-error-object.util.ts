import { isArrayOf, isArrayOfStrings, isObject } from './type-guards.util';

type MessageError = string | undefined;
type DescriptionError = string | string[] | undefined;
type StatusError = number | string | undefined;

export interface ErrorObject {
  message?: MessageError;
  description?: DescriptionError;
  status?: StatusError;
}

interface PossibleError {
  data?: string | PossibleErrorData;
  message?: string;
  error?: string;
  status?: number | string;
}

interface PossibleErrorData {
  message?: string;
  title?: string;
  details?: string | string[] | DetailsValidationError[];
  code?: string;
  statusCode?: number;
}

interface DetailsValidationError {
  code: string;
  message: string;
}

export function parseErrorObject(error: unknown): ErrorObject {
  if (!error) {
    return {};
  }

  if (typeof error === 'string') {
    return { message: error };
  }

  const possibleError = error as PossibleError;

  return {
    message: getMessageError(possibleError),
    description: getDescriptionError(possibleError),
    status: getStatusError(possibleError),
  };
}

function getMessageError(error: PossibleError): MessageError {
  if (typeof error.data === 'string') {
    return error.data;
  } else if (error.data?.message) {
    return error.data.message;
  } else if (error.data?.title) {
    return error.data.title;
  } else if (typeof error.data?.details === 'string') {
    return error.data.details;
  } else if (error.message) {
    return error.message;
  } else if (error.error) {
    return error.error;
  }

  return undefined;
}

function getDescriptionError(error: PossibleError): DescriptionError {
  if (typeof error.data === 'string' && error.message) {
    return error.message;
  } else if (typeof error.data === 'string' && error.error) {
    return error.error;
  } else if (
    typeof error.data !== 'string' &&
    error.data?.message &&
    isArrayOfStrings(error.data.details)
  ) {
    return error.data.details;
  } else if (
    typeof error.data !== 'string' &&
    isArrayOf<DetailsValidationError>(error.data?.details, isValidationError)
  ) {
    return error.data.details.map((item) => item.message);
  }

  return undefined;
}

function getStatusError(error: PossibleError): StatusError {
  if (error.status) {
    return error.status;
  } else if (error.data && typeof error.data !== 'string') {
    if (error.data.code) {
      return error.data.code;
    } else if (error.data.statusCode) {
      return error.data.statusCode;
    }
  }

  return undefined;
}

function isValidationError(value: unknown): value is DetailsValidationError {
  if (!isObject(value)) {
    return false;
  }

  return (
    'code' in value &&
    typeof value.code === 'string' &&
    'message' in value &&
    typeof value.message === 'string'
  );
}
