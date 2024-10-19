require('dotenv').config();
const express = require('express');
const { google } = require('googleapis');
const nodemailer = require('nodemailer');
const twilio = require('twilio');

const app = express();
app.use(express.json());

const PORT = process.env.PORT || 3000;


const twilioClient = twilio(process.env.TWILIO_ACCOUNT_SID, process.env.TWILIO_AUTH_TOKEN);


const calendar = google.calendar('v3');
const auth = new google.auth.OAuth2(
    process.env.GOOGLE_CLIENT_ID,
    process.env.GOOGLE_CLIENT_SECRET,
    process.env.GOOGLE_REDIRECT_URL
);
auth.setCredentials({ refresh_token: process.env.GOOGLE_REFRESH_TOKEN });


async function createGoogleCalendarEvent(appointmentDetails) {
    const calendarEvent = {
        summary: 'Hair Salon Appointment',
        location: 'Riya\'s Salon',
        description: appointmentDetails.description,
        start: {
            dateTime: appointmentDetails.startTime, 
            timeZone: 'Asia/Kolkata',
        },
        end: {
            dateTime: appointmentDetails.endTime, 
            timeZone: 'Asia/Kolkata',
        },
        reminders: {
            useDefault: false,
            overrides: [
                { method: 'email', minutes: 1440 }, 
                { method: 'popup', minutes: 10 }, 
            ],
        },
    };

    try {
        const response = await calendar.events.insert({
            auth: auth,
            calendarId: 'primary',
            resource: calendarEvent,
        });
        console.log('Event created: %s', response.data.htmlLink);
    } catch (error) {
        console.error('Error creating calendar event:', error);
    }
}


async function sendEmail(appointmentDetails) {
    const transporter = nodemailer.createTransport({
        service: 'gmail',
        auth: {
            user: process.env.EMAIL_USER,
            pass: process.env.EMAIL_PASS,
        },
    });

    const mailOptions = {
        from: process.env.EMAIL_USER,
        to: appointmentDetails.email,
        subject: 'Appointment Confirmation',
        text: `Your appointment is scheduled for ${appointmentDetails.startTime}.`,
    };

    try {
        await transporter.sendMail(mailOptions);
        console.log('Email sent successfully');
    } catch (error) {
        console.error('Error sending email:', error);
    }
}


async function sendSMS(appointmentDetails) {
    const message = await twilioClient.messages.create({
        body: `Your appointment at Riya's Salon is confirmed for ${appointmentDetails.startTime}.`,
        from: process.env.TWILIO_PHONE_NUMBER,
        to: appointmentDetails.phone,
    });

    console.log('SMS sent successfully:', message.sid);
}


app.post('/book-appointment', async (req, res) => {
    const appointmentDetails = req.body;
    
    await createGoogleCalendarEvent(appointmentDetails);
    await sendEmail(appointmentDetails);
    await sendSMS(appointmentDetails);

    res.status(200).json({ message: 'Appointment booked successfully!' });
});

app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});
