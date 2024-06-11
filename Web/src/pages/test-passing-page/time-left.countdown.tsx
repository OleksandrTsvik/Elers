import { Spin, Statistic } from 'antd';
import dayjs from 'dayjs';
import { useTranslation } from 'react-i18next';
import { useNavigate, useParams } from 'react-router-dom';

import { useFinishTestMutation } from '../../api/tests.api';
import { ErrorAlert } from '../../common/error';
import { isNumber } from '../../utils/helpers';

interface Props {
  startedAt: Date;
  timeLimitInMinutes?: number;
  testSessionId: string;
  testId: string;
}

export default function TimeLeftCountdown({
  startedAt,
  timeLimitInMinutes,
  testSessionId,
  testId,
}: Props) {
  const { courseId } = useParams();
  const navigate = useNavigate();

  const { t } = useTranslation();

  const [finishTest, { isLoading, error }] = useFinishTestMutation();

  const handleFinish = async () => {
    await finishTest({ testSessionId })
      .unwrap()
      .then(() => navigate(`/courses/${courseId}/test/${testId}`));
  };

  if (!isNumber(timeLimitInMinutes)) {
    return null;
  }

  return (
    <>
      <Spin
        fullscreen
        spinning={isLoading}
        tip={t('test_passing_page.finishing_test')}
      />

      <ErrorAlert className="mb-field" error={error} />

      <Statistic.Countdown
        className="text-right"
        title={t('test_passing_page.time_left')}
        value={dayjs(startedAt).add(timeLimitInMinutes, 'minute').toString()}
        onFinish={handleFinish}
      />
    </>
  );
}
