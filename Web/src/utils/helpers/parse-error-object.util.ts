type MessageError = string | undefined;
type DescriptionError = string | string[] | undefined;
type StatusError = number | string | undefined;

export interface ErrorObject {
  message?: MessageError;
  description?: DescriptionError;
  status?: StatusError;
}

export default function parseErrorObject(error: unknown): ErrorObject {
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

interface PossibleErrorData {
  error?: string;
  message?: string;
  description?: string;
  code?: string;
}

interface PossibleError {
  data?: string | PossibleErrorData;
  message?: string;
  error?: string;
  stack?: string;
  status?: number | string;
  code?: number | string;
}

function getMessageError(error: PossibleError): MessageError {
  if (typeof error.data === 'string') {
    return error.data;
  } else if (error.data?.message) {
    return error.data.message;
  } else if (error.data?.description) {
    return error.data.description;
  } else if (error.message) {
    return error.message;
  } else if (error.error) {
    return error.error;
  }

  return undefined;
}

function getDescriptionError(error: PossibleError): DescriptionError {
  if (typeof error.data === 'string' && error.error) {
    return error.error;
  } else if (
    typeof error.data !== 'string' &&
    error.data?.message &&
    error.data.description
  ) {
    return error.data.description;
  } else if (error.stack) {
    return error.stack;
  }

  return undefined;
}

function getStatusError(error: PossibleError): StatusError {
  if (error.status) {
    return error.status;
  } else if (error.code) {
    return error.code;
  } else if (typeof error.data !== 'string' && error.data?.code) {
    return error.data?.code;
  }

  return undefined;
}
