import { Alert } from 'antd';
import React from 'react';

import { ErrorMessage } from '..';

interface Props {
  error: unknown;
  style?: React.CSSProperties;
}

export default function ErrorAlert({ error, style }: Props) {
  return (
    <Alert
      type="error"
      style={style}
      message={<ErrorMessage error={error} />}
    />
  );
}
