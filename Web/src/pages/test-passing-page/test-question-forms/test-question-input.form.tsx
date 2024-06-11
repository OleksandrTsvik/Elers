import { Button, Input } from 'antd';
import { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';

import { useSendAnswerToTestQuestionMutation } from '../../../api/tests.api';
import { ErrorAlert } from '../../../common/error';

interface Props {
  questionId: string;
  userAnswer?: string;
}

export default function TestQuestionInputFrom({
  questionId,
  userAnswer,
}: Props) {
  const { testSessionId } = useParams();
  const { t } = useTranslation();

  const [answer, setAnswer] = useState(userAnswer);

  const [sendAnswer, { isLoading, error }] =
    useSendAnswerToTestQuestionMutation();

  useEffect(() => {
    setAnswer(userAnswer);
  }, [userAnswer]);

  const handleSubmit = async () => {
    await sendAnswer({ testSessionId, questionId, answer }).unwrap();
  };

  return (
    <>
      <ErrorAlert className="mb-field" error={error} />

      <Input
        className="mb-field"
        value={answer}
        onChange={({ target }) => setAnswer(target.value)}
      />

      <Button
        className="right-btn"
        type="primary"
        loading={isLoading}
        onClick={handleSubmit}
      >
        {t('test_passing_page.save_answer')}
      </Button>
    </>
  );
}
