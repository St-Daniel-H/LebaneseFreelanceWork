import Notifications from "./@Notifications/page";

export const metadata = {
  title: "Next.js",
  description: "Generated by Next.js",
};
type Types = {
  children: React.ReactNode;
  Notifications: React.ReactNode;
};
export default function RootLayout({ children, Notifications }: Types) {
  return (
    <html lang="en">
      <body>
        {children}
        {Notifications}
      </body>
    </html>
  );
}
