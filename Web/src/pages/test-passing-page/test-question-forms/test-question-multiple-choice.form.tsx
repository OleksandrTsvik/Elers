import { Checkbox, Flex, Spin } from 'antd';
import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

import { useSendAnswerToTestQuestionMutation } from '../../../api/tests.api';
import { ErrorAlert } from '../../../common/error';

interface Props {
  questionId: string;
  options: string[];
  userAnswers?: string[];
}

export default function TestQuestionMultipleChoiceFrom({
  questionId,
  options,
  userAnswers,
}: Props) {
  const { testSessionId } = useParams();

  const [answer, setAnswer] = useState(userAnswers);

  const [sendAnswer, { isLoading, error }] =
    useSendAnswerToTestQuestionMutation();

  useEffect(() => {
    setAnswer(userAnswers);
  }, [userAnswers]);

  const handleSubmit = async (value: string[]) => {
    setAnswer(value);

    await sendAnswer({ testSessionId, questionId, answers: value }).unwrap();
  };

  return (
    <Spin spinning={isLoading}>
      <ErrorAlert error={error} />

      <Checkbox.Group value={answer} onChange={handleSubmit}>
        <Flex vertical gap="small">
          {options.map((item, index) => (
            <Checkbox key={index} value={item}>
              {item}
            </Checkbox>
          ))}
        </Flex>
      </Checkbox.Group>
    </Spin>
  );
}
