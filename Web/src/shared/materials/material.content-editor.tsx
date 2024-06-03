import { Button } from 'antd';
import { useState } from 'react';

import { ErrorAlert } from '../../common/error';
import { TextEditor } from '../../common/typography';
import { classnames } from '../../utils/helpers';

interface Props {
  initialText?: string;
  textOnSubmitButton: string;
  isLoading: boolean;
  error: unknown;
  onSubmit: (text: string) => Promise<void> | void;
}

export function MaterialContentEditor({
  initialText = '',
  textOnSubmitButton,
  isLoading,
  error,
  onSubmit,
}: Props) {
  const [text, setText] = useState(initialText);

  const handleSubmit = () => onSubmit(text);

  return (
    <>
      <ErrorAlert className="mb-field" error={error} />
      <TextEditor editorKey="text" value={text} onChange={setText} />
      <Button
        className={classnames('right-btn', 'mt-field')}
        type="primary"
        disabled={!text}
        loading={isLoading}
        onClick={handleSubmit}
      >
        {textOnSubmitButton}
      </Button>
    </>
  );
}
