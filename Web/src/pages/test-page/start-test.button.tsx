import { App, Button } from 'antd';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useStartTestMutation } from '../../api/tests.api';
import { ErrorAlert } from '../../common/error';

interface Props {
  courseId: string | undefined;
  testId: string;
}

export default function StartTestButton({ courseId, testId }: Props) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const { modal } = App.useApp();
  const [startTest, { error }] = useStartTestMutation();

  const handleClick = async () => {
    await modal.confirm({
      title: t('course_test.start_test'),
      onOk: () =>
        startTest({ testId })
          .unwrap()
          .then((testSessionId) =>
            navigate(`/courses/${courseId}/test/attempt/${testSessionId}`),
          ),
    });
  };

  return (
    <>
      <ErrorAlert className="mb-field" error={error} />

      <Button block type="primary" onClick={handleClick}>
        {t('course_test.start_test')}
      </Button>
    </>
  );
}
