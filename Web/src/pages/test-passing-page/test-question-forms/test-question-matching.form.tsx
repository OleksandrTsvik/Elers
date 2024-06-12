import { Button, Form, Select } from 'antd';
import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';

import { useSendAnswerToTestQuestionMutation } from '../../../api/tests.api';
import { ErrorAlert } from '../../../common/error';
import { AnswerMatchOption } from '../../../models/test.interface';

interface Props {
  questionId: string;
  questions: string[];
  answers: string[];
  userAnswers?: AnswerMatchOption[];
}

export default function TestQuestionMatchingFrom({
  questionId,
  questions,
  answers,
  userAnswers,
}: Props) {
  const { testSessionId } = useParams();
  const { t } = useTranslation();

  const [form] = Form.useForm();

  useEffect(() => {
    form.setFieldsValue(
      userAnswers?.reduce(
        (accumulator, { question, answer }) => ({
          ...accumulator,
          [question]: answer,
        }),
        {},
      ),
    );
  }, [form, userAnswers]);

  const [sendAnswer, { isLoading, error }] =
    useSendAnswerToTestQuestionMutation();

  const handleSubmit = async (values: {
    [question: string]: string | undefined;
  }) => {
    const matchOptions = Object.entries(values).map(([question, answer]) => ({
      question,
      answer,
    }));

    await sendAnswer({ testSessionId, questionId, matchOptions }).unwrap();
  };

  return (
    <>
      <ErrorAlert className="mb-field" error={error} />

      <Form form={form} layout="vertical" onFinish={handleSubmit}>
        {questions.map((question, index) => (
          <Form.Item key={index} label={question} name={question}>
            <Select
              options={answers.map((answer) => ({
                label: answer,
                value: answer,
              }))}
            />
          </Form.Item>
        ))}

        <Form.Item>
          <Button
            className="right-btn"
            type="primary"
            htmlType="submit"
            loading={isLoading}
          >
            {t('test_passing_page.save_answer')}
          </Button>
        </Form.Item>
      </Form>
    </>
  );
}
