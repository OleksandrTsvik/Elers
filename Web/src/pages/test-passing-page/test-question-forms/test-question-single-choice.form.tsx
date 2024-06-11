import { Flex, Radio, Spin } from 'antd';
import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

import { useSendAnswerToTestQuestionMutation } from '../../../api/tests.api';
import { ErrorAlert } from '../../../common/error';

interface Props {
  questionId: string;
  options: string[];
  userAnswer?: string;
}

export default function TestQuestionSingleChoiceFrom({
  questionId,
  options,
  userAnswer,
}: Props) {
  const { testSessionId } = useParams();

  const [answer, setAnswer] = useState(userAnswer);

  const [sendAnswer, { isLoading, error }] =
    useSendAnswerToTestQuestionMutation();

  useEffect(() => {
    setAnswer(userAnswer);
  }, [userAnswer]);

  const handleSubmit = async (value: string) => {
    setAnswer(value);

    await sendAnswer({ testSessionId, questionId, answer: value }).unwrap();
  };

  return (
    <Spin spinning={isLoading}>
      <ErrorAlert className="mb-field" error={error} />

      <Radio.Group
        value={answer}
        onChange={({ target }) => handleSubmit(target.value as string)}
      >
        <Flex vertical gap="small">
          {options.map((item, index) => (
            <Radio key={index} value={item}>
              {item}
            </Radio>
          ))}
        </Flex>
      </Radio.Group>
    </Spin>
  );
}
