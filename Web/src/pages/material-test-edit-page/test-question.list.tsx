import { Radio, RadioChangeEvent, Skeleton, Spin, Typography } from 'antd';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';

import TestQuestionForm from './test-question.form';
import { useGetTestQuestionIdsAndTypesQuery } from '../../api/test-questions.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';
import { TestQuestionType } from '../../models/test-question.interface';

const defaultQuestionType = TestQuestionType.Input;

interface Props {
  testId: string;
}

export default function TestQuestionList({ testId }: Props) {
  const { t } = useTranslation();

  const [questionId, setQuestionId] = useState<string>();
  const [questionType, setQuestionType] = useState(defaultQuestionType);

  const { data, isLoading, isFetching, error } =
    useGetTestQuestionIdsAndTypesQuery({
      testId,
    });

  const handleChangeQuestionId = ({ target }: RadioChangeEvent) => {
    const id = target.value as string | undefined;

    setQuestionId(id);

    if (id && data) {
      setQuestionType(
        data.find((item) => item.id == id)?.type ?? defaultQuestionType,
      );
    } else {
      setQuestionType(defaultQuestionType);
    }
  };

  if (isLoading) {
    return (
      <>
        <Typography.Title level={2}>
          {t('course_test.test_questions')}
        </Typography.Title>
        <Skeleton active />
      </>
    );
  }

  if (error) {
    return <NavigateToError error={error} />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <Typography.Title level={2}>
        {t('course_test.test_questions')}
      </Typography.Title>

      <Spin spinning={isFetching} tip={t('loading.changes')}>
        <Radio.Group
          className="square-radio mb-field"
          buttonStyle="solid"
          value={questionId}
          onChange={handleChangeQuestionId}
        >
          {data.map(({ id }, index) => (
            <Radio.Button key={id} value={id}>
              {index + 1}
            </Radio.Button>
          ))}
          <Radio.Button value={undefined}>+</Radio.Button>
        </Radio.Group>

        <TestQuestionForm
          testId={testId}
          questionId={questionId}
          questionType={questionType}
          resetQuestionId={() => setQuestionId(undefined)}
          onChangeQuestionType={setQuestionType}
        />
      </Spin>
    </>
  );
}
